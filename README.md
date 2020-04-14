# HospitalProjectTeam4
Hospital Name: St. Joseph General Hospital Elliot Lake

Golden rules for team members:
* Pull your code first
* Update-Database
* Add-Migrations
* Push your code

Team Members:
* Andrea Villegas               Student No. N01401609
* Manpreet Kaur                 Student No. N01355663
* Zameer Fakirmohmed Chariwala  Student No. N01338717
* Yegor Fomin                   Student No. N01361126
* Rosario Hernandez             Student No. N01274803

Documentation per each member:
- Wireframes
- Narratives (Case Story)
- Entity Relationship Diagrams.

Team Member Features and Contributions:

Andrea Villegas: (Team Leader) 
* Records 
  - Description: 
  - Files
    * Models:
    * Views:
    * Controller:
 
* PatientForum 
  - Description:
  - Files:
    * Models:
    * Views:
    * Controller
    
* Contribution: Assisted and Resolved Git and Migration Conflicts during the whole process.
    
Manpreet Kaur
* News Section
  - Description: The news section feature is for users to read about the hospital.The users can only read the news in both list view and also in the single view read news by clicking on the newsname on the list page of news. They can't update and delete news from the hospital website.
  while, the admins can create, read, update and delete the news whenever they want.
-User story: Simmi can read the news from the hospital newssection in both list view and also in individual view. While, Jimmy is the admin of the hospital website. He can create a new news, update any old news and delete any news. Moreover, he can also read news in both list and single read view like Simmi.
For news, i also implemented the category CRUD. There is one category to many news relationship.But, only admin can use this full category CRUD for creating, reading, updtaing and deleting a category for news section. The users can't access this category CURD.
  - Files
   * Models: News.cs, Category.cs
   * Views: 
   News- New.cshtml, List.cshtml, Show.cshtml, Update.cshtml, DeleteConfirm.cshtml
   Category- Add.cshtml, List.cshtml, Show.cshtml, Update.cshtml, DeleteConfirm.cshtml
   * Controller- NewsController.cs, CategoryController.cs
 
* Donations
  - Description: The admin can create, read, update and delete news.
  The user can make a donation(non-registered and registered both)
  But, the registered can also read his/her donation details in the future by going to his/her account.
  All of the doctors, patients, users(non-registered) can only make donations (create) to the hospital site.
  User story-  Simmi a registered user can make donation to the hospital website. She, can see her donation details in her account as she is registered. But, Geeta non-registered will not be able to see her donation in future but she can only make a donation to the hospital site.
  Kim admin can create, read, update and delete any donation if she wants.
  - Files:
    * Models: Donation.cs
    * Views: Donation
   Add.cshtml
    * Controller: DonationController.cs
    I  tried to do this feature but the identity user conceptwas confusing for me. So, i was unable to finish this feature.
* Contribution:I created mine pages checked every content when others made commits and i tested database and helped Andrea by testing the commits.
    
Zameer Chariwala
* Booking Appointment
  - Description: 
  - Files
    * Models:
    * Views:
    * Controller:
 
* Lost and Found
  - Description:
  - Files:
    * Models:
    * Views:
    * Controller
    
* Contribution:
    
Rosario Hernandez
* Online Check-In

- Description: Registered / Logged-In users have access to this feature in order to add, view a list of, and delete a Check-In that       is related with a specific booking appointment. Administrators have full access to  Online Check-In  where they can create, view,       edit and delete an online check-In.
  
- User Story: Jake Murray has an appointment for today at 4:00 pm with his busy orthopedist at St. Joseph General Hospital, 3rd Floor.     Jake has arrived at the hospital at 3:55 pm and in order to keep his appointment, he logs in at the St. Joseph Hospital website         through his smartphone and checks-in for his appointment. Dr. Johnson's assistant can see in the offices database that Jake has         checked-in and they are both ready for his check-up.

  - Files
    * Models: OnlineCheckIn.cs
    * Views: Add.cshtml, Delete.cshtml, List.cshtml, Show.cshtml, Update.cshtml
    * Controller: OnlineCheckInController.cs
 
* Careers Form
  - Description: This feature represents
  - Files:
    * Models: JobPosting.cs, JobDepartment.cs, JobType.cs, CareersForm.cs
    * Views:
    * Controller: JobPostingController.cs, CareersFormController.cs
    
* Contribution: Readme File.

Yegor Fomin
* Articles
  - Description: 
  - Files
    * Models:
    * Views:
    * Controller:
 
* Parking
  - Description:
  - Files:
    * Models:
    * Views:
    * Controller
    
* Contribution:
  
  
 















