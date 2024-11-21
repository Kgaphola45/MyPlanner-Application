# MyPlanner Application

## Introduction
Welcome to the MyPlanner application! This web-based study planner assists students in organizing their modules and study hours efficiently. The application allows users to create study schedules, track progress, and store important academic data in a seamless and user-friendly manner.

This document provides detailed instructions for compiling, running the software, and setting up the necessary database to get the MyPlanner application up and running on your machine.

---

## 1. Getting Started

Before you begin, ensure you have the following tools and software installed on your machine:

### Prerequisites
- **.NET Core SDK**: This application is built on .NET Core, and you need the SDK to compile and run it. 
  - Download and install the .NET Core SDK from [dotnet.microsoft.com](https://dotnet.microsoft.com/download).
  
- **Microsoft SQL Server**: MyPlanner uses SQL Server to store and retrieve data.
  - Install and set up a SQL Server instance. You can download SQL Server from [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

- **Visual Studio or Code Editor**: Use Visual Studio or your preferred code editor to open and run the application.

---

## 2. Installation

### Steps to Set Up
1. **Download Project**: Download the MyPlanner project from the RC Learn portal.
   
2. **Open in Code Editor**: Navigate to the project folder and open the solution file (`MyPlanner.sln`) in Visual Studio or your preferred code editor.

3. **Update Connection Strings**:
   - Open the files `IndexModel.cs` and `PlannerModel.cs`.
   - Update the connection strings in these files with your SQL Server details. Look for the lines containing the connection strings and replace them with your SQL Server information.

---

## 3. Database Setup

### 1. Create Database:
- Open **SQL Server Management Studio (SSMS)** and execute the following SQL query to create the database:
   ```sql
   CREATE DATABASE STUDY_PLANNER;
