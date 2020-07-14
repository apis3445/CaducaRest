call newman run PostmanTesting.postman_collection.json -x --reporters junit Results\PostmanJunit.xml
call newman run test.postman_collection.json -x -r htmlextra --reporters junit --reporter-junit-export Results\junitReport2.xml 
call newman run Login/Login.postman_collection.json -e Login/Login.Azure.postman_environment.json -d Login/login.data.csv -x -r junitfull --reporter-junitfull-export Results/junitReport3.xml 
call newman run Login/Login.postman_collection.json -e Login/Login.Azure.postman_environment.json -d Login/login.data.csv -x -r htmlextra --reporter-htmlextra-skipSensitiveData --reporter-htmlextra-showMarkdownLinks Results/report.html
call newman run Login/Login.postman_collection.json -e Login/Login.Azure.postman_environment.json -d Login/login.data.csv --reporters cli,junit --reporter-junit-export Results\junitReport.xml 
