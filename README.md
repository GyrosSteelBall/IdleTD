# Tower Defense Game Project
This Unity-based project demonstrates our skills in game development, software architecture, and C# programming. Below are the key features and systems implemented in this project.

## Key Features
### 1. Enemy System

* Enemies spawn in waves and follow predefined paths
* Different enemy types with unique attributes (health, speed, etc.)
* Enemy state machine for managing behavior (moving, attacking)

### 2. Wave Management

* Multiple waves of enemies with increasing difficulty
* Wave data defined using ScriptableObjects for easy configuration
* Start wave button to give players control over wave timing

[![Image from Gyazo](https://i.gyazo.com/6c0bd97ef77ee90792b8ac4b58256a44.gif)](https://gyazo.com/6c0bd97ef77ee90792b8ac4b58256a44)

### 3. Tower/Unit System

* Players can place defensive units on the map
* Units have different attributes (attack damage, range, attack speed)
* Units automatically target and attack nearby enemies

### 4. Combat System

* Real-time combat between units and enemies
* Damage calculation and health management
* Visual feedback for attacks and damage taken

[![Image from Gyazo](https://i.gyazo.com/52044fc76b63f2854bf776c163761891.gif)](https://gyazo.com/52044fc76b63f2854bf776c163761891)

### 5. Resource Management

* Gold system for purchasing and upgrading units
* Lives system to track player health

### 6. Unit Placement and UI Systems

* Intuitive drag-and-drop interface for unit placement
* Dynamic range indicators for precise unit positioning
* Healthbars for enemies
* Unit selection hotbar for quick access to different unit types
* Upgrade buttons integrated into unit UI
* Terrain layer system to determine placable and non-placable locations

### 7. Upgrade System

* Three distinct upgrade paths for each unit type
* Multiple upgrades available within each path
* Upgrade effects dynamically applied to units
* Modular to ensure easier extendibility
  
[![Image from Gyazo](https://i.gyazo.com/9ed04dffabc9a26a9bd9b51b3df44e2f.gif)](https://gyazo.com/9ed04dffabc9a26a9bd9b51b3df44e2f)

### 8. Advanced Projectile System
* Diverse array of projectile behaviors (chaining, AOE, splitting, etc.) adding strategic depth to combat
* Projectile effects implemented through a flexible, modular design
* Projectile behavior is customizable through a scriptable object system
[![Image from Gyazo](https://i.gyazo.com/a8dd90bc1e2eed9ead8bd2e3b0b2f82e.gif)](https://gyazo.com/a8dd90bc1e2eed9ead8bd2e3b0b2f82e)

### 9. State Machine Architecture

* Robust state machine for managing game states (preparation, wave in progress, game over)
* Enemy and unit behavior controlled by individual state machines

### 10. Event-Driven Architecture

* Utilizes an event bus system for decoupled communication between game components
* Improves modularity and maintainability of the codebase

### 11. Singleton Pattern Implementation

* Efficient management of global game systems and managers

### Technical Highlights

* C# Programming: Demonstrates proficiency in C# and object-oriented programming principles
* Unity Engine: Leverages Unity's features for game development
* Design Patterns: Implements Singleton, State, and Factory patterns
* ScriptableObjects: Uses ScriptableObjects for data management and configuration
* Code Organization: Well-structured codebase with clear separation of concerns

### Development Branches
This project uses a branching strategy to manage feature development. Some features mentioned above may only be available on specific branches as they are still in development. 
