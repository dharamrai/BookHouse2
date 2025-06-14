This project, "Book House," serves as a practical demonstration of consuming a RESTful API within a .NET 8 Razor Pages web application. The primary goal is to showcase the end-to-end process of interacting with a backend API to perform standard CRUD (Create, Read, Update, Delete) operations and display the data on a user-friendly frontend.

This solution is designed for clarity and conciseness, focusing specifically on API consumption and data persistence, making it an ideal example for understanding core web development interactions without the added complexity of extensive architectural layers.

To run this project, do these:
1. Copy the HTTPS link of the project and clone the project via Visual Studio.
2. Set up the Db. You can find the Db script in the following location. Copy the script and run it in your Sqlserver database.
   > Infrastructure> Context> DBScript.txt

Technical Stack:
  Backend (WebApi Project):
      > .NET 8.0: Leveraging the latest features and performance enhancements of the .NET framework.
      
      > Entity Framework Core (ORM): For efficient and robust interaction with the database.
      
      > Entity Framework Core SQL Server: Specific provider for connecting to SQL Server databases.
      
      > AutoMapper: Streamlining object-to-object mapping between different layers (e.g., DTOs and entities).
      
      > Modular Structure: Separated into `Domain` (for business entities and interfaces) and `Infrastructure` (for data access implementations) class libraries to maintain logical organization. *Note: This project intentionally simplifies the architecture for demonstration purposes, omitting a dedicated service layer to highlight direct API-to-repository interaction.*
      
      > Frontend (Razor Pages):
          .NET 8.0 Razor Pages: For building dynamic web UI with a focus on page-based development.
          
      > CUBE Free Template: Utilized for a clean, modern, and responsive user interface, enhancing the overall presentation.
      
       > Database:
         SQL Server: As the robust relational database management system for storing book data.


<img width="795" alt="image" src="https://github.com/user-attachments/assets/53156618-cdc3-4535-b215-70376d88e334" />

<img width="839" alt="image" src="https://github.com/user-attachments/assets/c2915773-941d-44d4-998b-2c155ed5435f" />


Project structure:

<img width="638" alt="image" src="https://github.com/user-attachments/assets/7c70cc66-932d-4ea3-b1f6-5f6ae1a788b6" />


Start both the project (WebApi and Website) from the solution:

<img width="793" alt="image" src="https://github.com/user-attachments/assets/e7cc5dbd-ec6a-41d1-bab9-fd2934fdc651" />




