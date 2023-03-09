# Installation

1. Clone the application from github
1. Open the project in Visual Studio
1. Run the project
1. All is done! The database will be created automatically with basic data

# Connection strings

As you can see, we use SQLite database, which will be automatically be created in the project's files'.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=database.sqlite"
  },
```

# Created accounts

- Login: `admin` Password: `admin`
- Login: `user` Password: `user`

# Project overview

![[Pasted image 20230309223036.png]]

As you can see, unauthorized user doesn't see much. We can see only a list of courses.

![[Pasted image 20230309223119.png]]

![[Pasted image 20230309223139.png]]

![[Pasted image 20230309223207.png]]
After logging in as user, we can see which lessons are in the courses and we can access detailed course view.

![[Pasted image 20230309223231.png]]

If we log out and log in as admin, we have full access to the website

![[Pasted image 20230309223317.png]]

![[Pasted image 20230309223343.png]]

![[Pasted image 20230309223350.png]]

![[Pasted image 20230309223400.png]]

![[Pasted image 20230309223410.png]]

![[Pasted image 20230309223425.png]]

![[Pasted image 20230309223432.png]]

![[Pasted image 20230309223440.png]]