# Key Features
## 1. Enemy System

* Enemies spawn in waves and follow predefined paths
* Different enemy types with unique attributes (health, speed, etc.)
* Enemy state machine for managing behavior (moving, attacking)

## 2. Tower/Unit System

* Players can place defensive units on the map
* Units have different attributes (attack damage, range, attack speed)
* Units automatically target and attack nearby enemies

## 3. Wave Management

* Multiple waves of enemies with increasing difficulty
* Wave data defined using ScriptableObjects for easy configuration
* Start wave button to give players control over wave timing

## 4. Combat System

* Real-time combat between units and enemies
* Damage calculation and health management
* Visual feedback for attacks and damage taken

## 5. Resource Management

* Gold system for purchasing and upgrading units
* Lives system to track player health

## 6. State Machine Architecture

* Robust state machine for managing game states (preparation, wave in progress, game over)
* Enemy and unit behavior controlled by individual state machines

## 7. Event-Driven Architecture

* Utilizes an event bus system for decoupled communication between game components
* Improves modularity and maintainability of the codebase

## 8. Singleton Pattern Implementation

* Efficient management of global game systems and managers

## Technical Highlights

* C# Programming: Demonstrates proficiency in C# and object-oriented programming principles
* Unity Engine: Leverages Unity's features for game development
* Design Patterns: Implements Singleton, State, and Factory patterns
* ScriptableObjects: Uses ScriptableObjects for data management and configuration
* Code Organization: Well-structured codebase with clear separation of concerns

## Development Branches
This project uses a branching strategy to manage feature development. Some features mentioned above may only be available on specific branches as they are still in development. 
