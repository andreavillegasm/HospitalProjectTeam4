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

## Andrea Villegas: (Team Leader) 
* Records 
  - Description: The Records feature helps both doctors and patients to keep notes, prescriptions, and referral records from appointments for future reference. This feature is accessed through the show page of an appointment as when viewing the details of an appointment, a doctor can add a record and the patient can view it if they wish to review an appointment's details.
  - User Story: Doctor Florentino just had an appointment with the Patient Mariana. He has taken some notes as well as prescribed some medicine. Doctor Florentino goes into St.Joseph's General Hospital Page and goes to his appointments. There he can see the appointment with Mariana. Doctor Florentino views the appointment and chooses add record. He inputs the overview  of the record and selects type prescription. After he goes into edit and adds the attachement of the Prescription so it can be printed by Mariana if she wishes. 
  If he wishes, he can add more attachments to the same appointment, delete attachments or edit them.
  Once the Patient Mariana Logins she can see under her appointment with Doctor Florentino the prescription  and any other documents Doctor Florentino attached to the appointment, which now she can view and print.
  - Files
    * Models: Record.cs
    * Views: (Found under BookingAppointment): ShowRecord.cshtml, UpdateRecord.cshtml, DeleteRecord.cshtml, Show.cshtml (Insert Record        Part)
    * Controller: BookingAppointmentController.cs (All Record related methods)
 
* PatientForum 
  - Description:
  - Files:
    * Models:
    * Views:
    * Controller
    
* Contribution: Assisted and Resolved Git and Migration Conflicts during the whole process.
    
## Manpreet Kaur
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
    
## Zameer Chariwala
* Booking Appointment
  - Description: This feature is to book an appointment with any doctor in st.Joseph hospital. Where user will login and he can only see his/her appointment with doctor. They can update appointments and delete an appointments if they dont require anymore.
  
  -User story: John is a patient and he wants to see a doctor he wants to book an appointment with Dr.Jack so, he can open st.joseph hospital website and he can book an appointment with Dr.Jack. Another day he wants to see another in that hospital he can book an many appointments he wants with many doctors. When jack login into website he can only see his appointments and he can only edit and delete his appointments. Same way when Dr.Jack logins he can only see appointments made with him and he can update and delete the appointment.
  - Files
    * Models: (ViewModel)AddBooking.cs, (ViewModel)UpdateBooking.cs, Booking.cs.
    * Views: Add.cshtml, List.cshtml, show.cshtml, Update.cshtml, ListMyBooking.cshtml.
    * Controller: BookingAppointmentController.cs. 
 
* Lost and Found
  - Description: This feature is useful to any patient in hospital, if any patient found any lost item in hospital he can post it on website and people can search for their lost item can they contact to the person and collect the item. User can also search only lost,found or stolen items in it.
  
  -User story: William is a patient in hospital he found a mobile phone in corridor he doesn't know who is the owner he posted a report on st.joseph website with all phone details with image. Same time Lusy lost her mobile phone and she searched for found item on st.joseph website and she found her mobile phone she called on given number and colleted her item.
  - Files: 
    * Models: LostFound.cs.
    * Views: Add.cshtml, List.cshtml, Update.cshtml
    * Controller: LostFoundController.cs.
    
 * Registration: 
  - Description: This feature will enable any user to register themselves into the website and login into the website. Where user will input all required information like who is he and all other information and he/she can register themselves.
  
  -User: Dr Jack got a job in st.joseph hospital and now he need an account in website so that he can see his appointment and all stuff. Dr jack goes to the website and Fill out all required information and register himself. Same thing with patient John and Hospital staff nurse nancy.
  - Files:
    * Models: Doctor.cs, HospitalStaff.cs, Patient.cs.
    * Views: Add.cshtml.
    * Controller: RegistrationController.cs.
 
* Contribution: Helping out with errors.
    
## Rosario Hernandez
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

## Yegor Fomin
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
  
  
 















