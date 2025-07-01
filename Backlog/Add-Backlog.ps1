# === CONFIGURATION ===
$projectName = "ITWholesale"  # Remplace par le nom de ton projet .csproj sans extension
$csprojPath = "$projectName.csproj"
$backlogPath = "Backlog"

# === 1. Créer le dossier Backlog s’il n'existe pas ===
Write-Host "1- backlogPath :  $backlogPath"

if (-not (Test-Path $backlogPath)) {
    New-Item -ItemType Directory -Path $backlogPath  -Force | Out-Null
    if (Test-Path $backlogPath) {
        Write-Host " Dossier 'Backlog' créé."
    } else {
        Write-Host " Échec création dossier 'Backlog'."
        exit 1
    }
}

# === 2. Créer les fichiers Markdown ===

@{
    "Epics.md" = "# Épique : Gestion du panier avec promotions et suivi du stock"
    "UserStories.md" = "# User Stories`n`n## US1 - Ajout au panier`n..."
    "TechnicalTasks.md" = "# Tâches Techniques`n- Implémenter la classe Product`n..."
    "ProductBacklog.md" = "# Backlog Produit`n| ID | Titre | Priorité | Estimation |`n..."
    "Sprint1.md" = "# Sprint 1 - Planification`n## Objectif : Ajout produit + promo 1"
}.GetEnumerator() | ForEach-Object { 
    # .GetEnumerator() convertit le hashtable en une collection d’entrées clés/valeurs utilisables dans la boucle.
    # Dans la boucle, $_ représente une entrée (DictionaryEntry) qui a bien .Key et .Value.
    # Sans .GetEnumerator(), $_ est le hashtable entier, donc les propriétés n’existent pas comme attendu.

    Write-Host "2- backlogPath :  $backlogPath, Key : $($_.Key)"
	$filePath = Join-Path $backlogPath $_.Key
	Write-Host "filePath :  $filePath Value : $($_.Value)"
    Set-Content -Path $filePath -Value $_.Value
    Write-Host " Fichier '$($filePath)' créé."
}

# === 3. Ajouter au .csproj les fichiers Backlog ===
if (Test-Path $csprojPath) {
    $csprojContent = Get-Content $csprojPath -Raw
    if ($csprojContent -notmatch "Backlog\\\*\\\*\\\*\.md") {
        $newContent = $csprojContent -replace '</Project>', @"
  <ItemGroup>
    <None Include="Backlog\**\*.md" />
  </ItemGroup>
</Project>
"@
        Set-Content -Path $csprojPath -Value $newContent
        Write-Host " Fichiers Backlog/*.md ajoutés au projet .csproj ."
    } else {
        Write-Host " Le projet contient déjà une inclusion pour Backlog/*.md"
    }
} else {
    Write-Host " Fichier $csprojPath introuvable. Assure-toi d’être dans le bon dossier."
}
