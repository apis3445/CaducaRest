call newman run PostmanTesting.postman_collection.json -x --reporters cli,junit -r htmlextra --reporter-junit-export Results\junitReport.xml
call newman run test.postman_collection.json -x --reporters cli,junit --reporter-junit-export Results\junitReport2.xml
call newman run Login/Login.postman_collection.json -e Login/Login.Azure.postman_environment.json -d Login/login.data.csv -x --reporters cli,junit --reporter-junit-export Results\junitReport3.xml
