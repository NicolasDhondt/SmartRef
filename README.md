# SmartRef - Business & Decision (Stage)

### Resume

My internship took place at Business & Decision. It was a great opportunity for me to improve my skills and learning a lot of new things. I was able to get very good feedback on my work, my autonomy and my communication skills.

### What was expected of me

To create a web solution allowing the company to archive references of projects done for previous clients. These archives are to be used in the context of RFP (using PowerPoint). The site must allow the creation of new project references, to see all references with the possibility to filter them and allow the generation of PowerPoint files.

### Technologies and methodologies

I worked with Microsoft Azure DevOps in addition to ASP.NET Core, C# and Angular technologies. The architecture used is called "Clean Architecture". The agile method was used with the project managers.

### Work done

- Building the database: following a MockUp, I had to determine the entities, relations and configurations necessary to the realization of the project. I proceeded to a UML analysis which was validated before being implemented.

- Create a base API: command, queries, web controller using MediatR, DTO, mapping, pagination, ... were done to have the most complete API as possible to start right.

- Recover previous project references: to stop working with test data, I needed to retrieve the real data from the company. They were contained in an Excel file and several PowerPoint files.

- PowerPoint file generation: use and integration of the OpenXML SDK tool in the application to generate a PowerPoint file. This file can be downloaded with a name adapted according to the previous client related to the project.

- Create the application interface: using Angular and its component system, I was able to challenge the API with my services and create a responsive interface corresponding to the provided MockUp.

### To run the code

- First : ```cd src/WebUI```

- Second : ```dotnet watch run```

A proxy run before seeing the web app! It can take few seconds to complete the database.
