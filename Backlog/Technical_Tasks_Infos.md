# Technical Tasks - Why This File Exists?

## 📌 Purpose

This file documents the **technical implementation tasks** that stem from the user stories. It answers the question:

> "How will we technically implement this feature?"

---

## 🧠 Functional vs. Technical

In Agile Scrum:

- **User Stories** describe the *what* — what the user needs or wants.
- **Technical Tasks** describe the *how* — how the development team will build it.

---

## ✅ Why Maintain a TechnicalTasks.md File?

### 1. Helps Sprint Planning
Breaking down user stories into small, technical units helps estimate and assign work efficiently.

### 2. Improves Team Collaboration
Developers can pick up a technical task even if they don’t fully understand the business logic behind the story.

### 3. Ensures Traceability
You can ensure each user story has its technical implementation covered.

---

## 🧩 Example: From Story to Task

**User Story (US3) – Apply Promotions**

Corresponding technical tasks:
- Create interface `IPromotionRule`
- Implement `BuyOneGetOneFreePromotion`
- Implement `TenEuroPer250Rule`
- Build `PromotionEngine` to apply daily rules
- Write unit tests for each promotion

---

## 📄 Recommendation

Use this file to:
- Track technical breakdown of each story
- Map each task to its corresponding US (e.g., `US3a`)
- Serve as a bridge between functional planning and actual coding

