# Alertnity: Neighbourhood Guardian

![Logod](https://github.com/Layusmen/Alertnity/assets/67705870/87dfeaaf-457a-4827-ad31-79e3a4bd176b)

Welcome to **Alertnity**, the comprehensive platform designed to empower residents to share information, collaborate with local authorities, and enhance the safety of their neighbourhoods. This document provides an overview of the Alertnity system, its features, and guidelines for usage.

## Table of Contents

1. [Introduction](#introduction)
2. [Key Features](#key-features)
3. [Data Handling and Sources](#data-handling-and-sources)
4. [Functionality](#functionality)
5. [Data Requirements and Structure](#data-requirements-and-structure)
6. [Technology Stack](#technology-stack)
7. [APIs and Integrations](#apis-and-integrations)
8. [Permissions and User Roles](#permissions-and-user-roles)
9. [Setup and Installation](#setup-and-installation)
10. [Usage](#usage)
11. [Contributing](#contributing)
12. [License](#license)

---
![Neighbourhood Guardian Roadmap(1)](https://github.com/Layusmen/Alertnity/assets/67705870/5543b307-80bf-426c-b776-60364f43743b)



## Introduction

**Alertnity** is a neighbourhood guardian platform that facilitates secure communication among residents and collaboration with local authorities. It offers graphical tools to report incidents, share images and videos, and alert others about security concerns. The platform helps raise awareness of local safety issues through dynamic visualizations and community-driven features.

---

## Key Features

- **Crime Map**: View recent crime reports on an interactive map.
- **Heatmap Visualizations**: Identify high-incident areas with dynamic maps.
- **Police Guidelines and Real-time Alerts**: Access updates from local police, including alerts on suspicious activities.
- **Community Forum**: Discuss local issues and share information with fellow residents.
- **Emergency Contact List**: Quick access to important contacts like law enforcement and medical services.
- **Virtual Patrol**: Integrate with doorbell cameras to share footage securely.
- **House Watch Request**: Request neighbours to monitor your property during your absence.
- **Neighbourhood Events Calendar**: Keep track of community events and meetings.
- **Detailed Incident Reporting**: Report incidents with photos, videos, and detailed descriptions.
- **Pattern Recognition**: Use AI to detect patterns in crime reports.
- **Two-Way Messaging**: Communicate directly with neighbourhood watch or law enforcement.
- **Group Chats**: Facilitate discussions within specific neighbourhood groups.
- **Neighbourhood Directory**: Opt-in directory for sharing contact information.
- **Shared Resources Platform**: Exchange tools and resources with neighbours.
- **Pet Care Network**: Offer and find pet-sitting services.
- **Points and Badges**: Earn rewards for active participation.
- **Leaderboards**: Recognize top contributors to community safety.

---

## Data Handling and Sources

### Data Requirements

#### Crime Mapping
- **Essential Data**:
  - Month of Crime
  - Longitude & Latitude
  - Crime Category
  - Crime Outcome
- **Additional Data for Enhanced Functionality**:
  - Street
  - Postcode
  - Reported Date & Time
  - Crime Description (Optional)
  - Reporting Party (Optional)

#### User Data
- **Required Information**:
  - Name
  - Date of Birth
  - Profile Picture
  - Phone Number
  - Address
  - Postcode
  - UserID
  - Email
  - Password
  - Permissions

#### Neighbourhood Feed
- **Content Data**:
  - PostID
  - UserID/UserName
  - Timestamp
  - Postcode
  - Upvotes/Downvotes
  - Comments
  - Categories/Tags

#### Virtual Patrol
- **Authorization Data**:
  - Resident UserName
  - Security Clearance Level
  - Resident IP Camera Information
  - Authorization Status and Expiry

### Data Sources
- **Police API**: Crime reports and police updates.
- **Google Maps API**: Geolocation and mapping services.
- **UK Population API**: Demographic data.
- **Ordinance Postcode API**: Postcode lookup and validation.
- **Open Source Intelligence (OSINT) Tools**: Additional crime and security information.

---

## Functionality

### User Capabilities
- **Reporting**: Users can report incidents with text, photos, and videos.
- **Viewing**: Access maps and reports of local incidents.
- **Communication**: Participate in forums and group chats.
- **Notification**: Receive alerts about local safety issues.
- **Event Participation**: Engage with the neighbourhood through the events calendar.

### Admin Capabilities
- **Management**: Oversee user permissions and content.
- **Analysis**: Use pattern recognition for incident analysis.
- **Moderation**: Maintain the community forum and handle reports.

---

## Data Requirements and Structure

### Crime Data
- **CrimeInformation**:
  - `timeStamp`: Time of report
  - `reportedDate`: Date of report
  - `longitude`: Longitude
  - `latitude`: Latitude
  - `street`: Street name
  - `postcode`: Postcode
  - `CrimeCategory`: Enum of crime types
  - `CrimeOutcome`: Outcome of the crime
  - `CrimeDescription`: Optional description
  - `ReportingParty`: Enum of reporting entities

### User Data
- **User**:
  - `userID`: Unique identifier
  - `UserName`: Username
  - `FirstName`: First name
  - `LastName`: Last name
  - `PhoneNumber`: Phone number
  - `Postcode`: Postcode
  - `Email`: Email address
  - `Permissions`: Enum of permissions
  - `Timestamp`: Time of user action

### Neighbourhood Feed
- **PostContent**:
  - `postID`: Unique identifier
  - `userID`: User ID
  - `Content`: Post content
  - `Timestamp`: Time of posting
  - `Upvotes`: Number of upvotes
  - `Downvotes`: Number of downvotes
  - `Comments`: List of comments
  - `Category`: Post category

### Virtual Patrol
- **Authorization**:
  - `UserName`: Username
  - `SecurityClearanceLevel`: Clearance level
  - `ResidentAuthorization`: Authorization status
  - `AuthorizationExpiry`: Expiry date
  - `residentIpAddress`: Resident IP camera address

---

## Technology Stack

- **Frontend**:
  HTML, CSS, Javascript
  - Google Maps for mapping and visualizations.
- **Backend**:
  - C#, ASP.NET with API handling.
- **APIs**:
  - RESTful APIs for data communication.
  - External APIs for crime data, geolocation, and more.
- **AI and Machine Learning**:
  - Pattern recognition for crime data analysis.
- **Notifications**:
  - Push notifications for alerts and updates.
    
---

## APIs and Integrations

- **[Police API](https://data.police.uk/docs/api-call-limits/)**: For accessing crime reports and police updates.
- **[Google Maps API](https://developers.google.com/maps/documentation)**: For geolocation and mapping.
- **[UK Population API](https://www.nomisweb.co.uk/api/v01/help)**: For demographic data.
- **[Ordinance Postcode API](https://www.api.gov.uk/nd/ordnance-survey-places-api/#ordnance-survey-places-api)**: For postcode data.

---

## Permissions and User Roles

### Permissions
- **None**: No access.
- **Read**: View information.
- **Write**: Create or modify information.
- **Execute**: Perform actions.
- **Delete**: Remove resources.
- **Admin**: Manage system and users.
- **SuperAdmin**: Full access including admin rights.

### User Roles
- **Resident**: Standard user with reporting and viewing capabilities.
- **Moderator**: Manage content and user interactions.
- **Admin**: Oversee platform operations and user management.
- **SuperAdmin**: Full system control.

---

## Setup and Installation

1. **Clone the Repository**: `git clone https://github.com/alertnity/alertnity.git`
2. **Install Dependencies**: Navigate to the project directory and run `npm install`.
3. **Set Environment Variables**: Configure API keys and database connections in `.env` file.
4. **Run the Application**: Start the development server with `npm start`.
5. **Access the Application**: Open your browser and go to `http://localhost:3000`.

---

## Usage

1. **Sign Up**: Create an account with your personal details.
2. **Login**: Access your dashboard using your credentials.
3. **Report Incidents**: Use the report feature to submit incident details.
4. **View Maps**: Check the crime map and heatmap for local reports.
5. **Join Discussions**: Participate in community forums and group chats.
6. **Receive Alerts**: Get real-time notifications about neighbourhood safety.

---

## Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit and push to your branch.
5. Create a pull request.

---

## License

Alertnity is licensed under the MIT License. See `LICENSE` file for details.

---

For more information, visit our [documentation](https://github.com/layusmen/alertnity/wiki) or contact support at support@alertnity.com.
