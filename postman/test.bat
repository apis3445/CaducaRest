call newman run Login/Login.postman_collection.json -e Login/Login.Azure.postman_environment.json -d Login/login.data.csv --reporters cli,junit --reporter-junit-export Results\junitReport.xml -x
