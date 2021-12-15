******************************************************************************
*	Created By : Ishan Chikhalkar                                            *
*	Contact    : ishan.chikhalkar@atos.net                                     *
******************************************************************************
# HCA Lab Test
	This is a Web API application for lab test data handing and reporting developed using .Net Core 5.0

# Problem statement
	Need application that is capable of
	1. Creating/Managing Patient securely
	2. Creating/Managing Lab Test securely
	3. Added logging for the exceptions.
	

# Tables (As model classes for In-Memory DB implementation)
			
	Patient
        	int PatientId//PrimarKey
		string Name //Patient Name
		DateTime DateOfBirth //Date of birth of patient
                string Gender // Gender of the paitent.
				
	LabTest
		int LabTestId//PrimarKey
		int PatientId//Foregin Key
		Enum TypeOfTestId //Type of test (0 - glucoseTests, 1 - completeBloodCount,2 - lipidPanel , 3 - urinalysis)
		string Result Result of the test
		sring TimeOfTest //Time on which the test happend 
		string EnteredTime //Entered time
		DateTime DateTimeOfTest//Maximum limit of value for the result
				
	PatientLabTest
		int PatientId //PrimarKey
		LabTestId  //ForeignKey LabTest.Id
		string Gender 
		DateTime DateOfBirth
		
# Approach
	Implemention using In-Memory DB
	Patient details in Patient class
	Test details in LabTest class
	Report details in PatientLabTest class
	Delete is hard delete 
		
#Operations Supported with endpoints
	Operations supported with endpoint details, sample URL and payload information 
	
	1. Endpoint Patient
		* Create : (Post : https://localhost:44367/Patient/Create)
			{
				"patientId": 0,
				"Name": "Test Patient 1",
				"dateOfBirth": "1980-05-25T00:00:00",
				"Gender": "M",
			}
		* Update : (Put : https://localhost:44367/Patient/Update/1)
			{
				"patientId": 0,
				"Name": "Test Patient 1",
				"dateOfBirth": "1980-05-25T00:00:00",
				"Gender": "F",
			}
		* Delete : (Delete : https://localhost:44367/Patient/Delete/1)
		* GetAll : (Get : https://localhost:44367/Patient/Get)
		* GetById : (Get : https://localhost:44367/Patient/Get/1)
		* GetPatientViewModelsForSearchingCriteria : (Get : https://localhost:44367/Patient/Get/{typeOfTestId}/{dateFrom}/{dateTo})
					
	2. Endpoint LabTest
		* Create : (Post : https://localhost:44367/LabTest/Create)
			{
				"LabTestId": 0,
				"patientId": 0,
				"testType": 1,
				"Result": "string",
				"TimeOfTest" : "11:30",
				"EnteredTime" : "12:30",
				"DateTimeOfTest" : "2021-10-10 11:30 AM"
			}		
		* Update : (Put : https://localhost:44367/LabTest/Update/1)
			{
				"LabTestId": 0,
				"patientId": 0,
				"testType": 1,
				"Result": "string",
				"TimeOfTest" : "11:30",
				"EnteredTime" : "12:30",
				"DateTimeOfTest" : "2021-10-10 11:30 AM"
			}		
		* Delete : (Delete : https://localhost:44367/LabTest/Delete/1)
		* Restore : (Put : https://localhost:44367/LabTest/Restore/1)
		* GetAll : (Get : https://localhost:44367/LabTest/Get)
		* GetById : (Get : https://localhost:44367/LabTest/Get/1)
		
		
#Installation
	1. Copy code in a folder
	2. Open LabTest soluion using Microsoft Visual Studio (LabTest.sln)
	3. Build and Run project LabTest
	4. Application should run in browser using Swagger UI
	5. Postman can also be configured (as per above url and payload details) for generating and passing token
	
#Steps to run with Swagger
	1. Create/Update/Delete/Get Patient data.
	2. Create/Update/Delete/Get LabTest data.
	
#Steps to run with Postman
	1. Configure Postman requests as per information above
	2. Follow sequence as below to handle data dependencies 
	3. Create Patient
	4. Create LabTest 
	5. Generate lab report
