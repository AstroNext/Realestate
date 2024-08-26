# RealEstate
- Add / remove / restore payment type
- Add / remove / restore house type
- Add / remove / restore zone
- Add / remove / restore house with rent price, sell price
- You can export your contract
- Export to excel your cotnracts

  ### Important
1) To run this application you need to add your Miscrosoft SQL server connection string to app config.
Database created automaticly. [here](https://github.com/AstroNext/Realestate/blob/master/RealEstate/App.config)
```xml
<connectionStrings>
  <clear/>
  <add name="Context" connectionString="Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=RealEstate;Data Source=.;Password=1234" providerName="System.Data.SqlClient"/>
</connectionStrings>
```
2) To generate new license to active this app run from [here](https://github.com/AstroNext/LicenceKey)
3) in lincense application add day and password then click generate and check validate
4) now copy hash string and replace with secret phase in [here](https://github.com/AstroNext/Realestate/blob/master/RealEstate/EncriptPassword.cs#L11)

```c#
public string secretPhase()
{
    return "E6V+QQkHXx5OKWoRDulFSA==";
}
```
  
## Login
![Login](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/login.png)

## Dashboard
![Dashboard](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/dashboard.png)

## Add
![Add 1](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/add%20payment%20type.png)
![Add 2](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/add%20type.png)
![Add 3](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/add%20zone.png)
![Add 4](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/add.png)
![Add 5](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/add_2.png)

## Report
![Add 6](https://github.com/AstroNext/Realestate/blob/master/RealEstate/media/report.png)
