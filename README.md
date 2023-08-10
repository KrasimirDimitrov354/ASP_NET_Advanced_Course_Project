# The Sith Academy - An ASP.NET Advanced course project
## Introduction to the project
The Sith Academy is intended to emulate a real-world website for remote learning. Students (acolytes) can remotely access different schools (academies), which themselves are located in different cities (locations). Each school has different courses (trials) to participate in. Students can submit their homeworks for a given course, which can then be reviewed and graded by a teacher (overseer). Students can become teachers in a school by manually being assigned to the role by the admin.

The project has added functionality which would not be found in the real world. For example, students can be restricted from joining or leaving a school.

Below you can find more information about each entity, their purpose and their functionality.

:construction: **THIS PROJECT IS STILL A WORK IN PROGRESS** :construction:\
Missing functionality:
- Unit testing
- Usage of file storage API for storing images 

## Different entities and their functions

###	:man_student::woman_student: **[AcademyUser](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/AcademyUser.cs)**
The AcademyUser entity represents a student (or acolyte in Sith terminology). Acolytes can join multiple academies, as long as:
- they are in the same location
- the academy is not locked by an overseer or an admin
- the location is not locked by an admin
The same requirements apply when an acolyte wants to leave an academy. If an acolyte successfully leaves every academy in a location, they can then join an academy in a different location.

There are four users seeded by default - DefaultAcolyte (password is 123), DreshdaeOverseer (password is 456), DarkTempleOverseer (password is 789) and Administrator (password is admin).

### :man_teacher::woman_teacher: **[Overseer](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Overseer.cs)**
The Overseer entity represents a teacher. An overseer is restricted to one academy, but they have access to much more functionality compared to an an acolyte. An overseer can:
- edit an academy's details (title, description and image url)
- lock an academy, thus preventing acolytes from joining or leaving it. This does not affect other academies in the same location
- add a course (trial) to the academy they are assigned to
- edit a trial's details, lock status and resources
- grade an acolyte's homework

Only the admin can promote acolytes to overseers and vice versa. The admin can also assign overseers to a different academy.

### :ringed_planet: **[Location](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Location.cs)**
The Location entity represent a city. Only the admin can perform CRUD operations on a location. Each location can host multiple academies, and when a location is locked, so is every academy in it.

### :school: **[Academy](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Academy.cs)**
The Academy entity represent a school. Each academy:
- exists in a set location
- can have multiple overseers assigned to it
- can have multiple acolytes attending it
- can host multiple trials
An academy's details can only be modified by the overseers assigned to it or by the admin. Only the admin can create new academies or assign/remove overseers from academies.

### **[AcademyAcolyte](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/AcademyAcolyte.cs)**
The AcademyAcolyte entity is a mapping table that tracks whether or not an acolyte is currently attending an academy.

### :crossed_swords: **[Trial](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Trial.cs)**
The Trial entity represent a school course. Each trial:
- is assigned to a set academy
- can have multiple resources
- has a ScoreToPass field, which determines the minimum score a homework needs to be graded at in order for an acolyte to pass the trial
A trial's lock status determines whether or not acolytes in the academy can submit or edit their homeworks.

### **[TrialAcolyte](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/TrialAcolyte.cs)**
The TrialAcolyte entity is a mapping table that tracks whether or not an acolyte has completed a given trial. This information is used in the acolyte's home page for easier access to the trials which have not yet been completed.

### :scroll: **[Resource](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Resource.cs)**
The Resource entity represents a resource which a real-world course would have. Instead of leading to the table of the elements or a spreadsheet with arithmetic formulas, it leads to an article thematically fitting for the given trial. The Resource entity is currently the only entity in the database which has a soft delete option. Every other entity cannot be removed from anywhere else except through direct modification of the database.

###  :writing_hand: **[Homework](https://github.com/KrasimirDimitrov354/ASP_NET_Advanced_Course_Project/blob/main/SithAcademy/SithAcademy.Data.Models/Homework.cs)**
The Homework entity represents the homework of an acolyte. Only the acolyte that has created the homework can preview or edit it. An overseer of the academy or the admin can score it, thus determining whether or not an acolyte passes a trial. Since the descriptions of the trials are a bit abstract (in the sense that they don't assign any actual activity to complete), the homework is a free-form text which is intended to be something of a summary or a short essay extracted from a trial's resources.
