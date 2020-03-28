cd postmanTesting
call newman run PostmanTesting.postman_collection.json -x --reporters cli,junit --reporter-junit-export Results\junitReport.xml
call newman run test.postman_collection.json -x --reporters cli,junit --reporter-junit-export Results\junitReport2.xml
