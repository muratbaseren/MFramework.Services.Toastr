
# nuget-mvc-toastr-service
MFramework MVC Toastr service for mvc 5 apps.

| Build server| Platform       | Status      |
|-------------|----------------|-------------|
| AppVeyor    | Windows        |[![Build status](https://ci.appveyor.com/api/projects/status/90h8oq7f4pftewy9?svg=true)](https://ci.appveyor.com/project/muratbaseren/nuget-mvc-toastr-service) |

## Nuget
> install-package MFramework.Toastr.Service

## First
Add Toastr CSS and JS to **_Layout**. Also add another CSS and JS files..

##### Adding styles to above head closing tag.
```html
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/font-awesome.min.css" rel="stylesheet" />
    <link href="~/Content/toastr.min.css" rel="stylesheet" />
```

##### Adding scripts to above body closing tag.

```html
    <script src="~/Scripts/jquery-1.6.5.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>
    <script src="~/Scripts/toastr.min.js"></script>
    <script src="~/Scripts/toastr.config.js"></script>
```

##### Then calling helper method.
This method generating and adding toastr scripts from read to ToastrService.
```csharp
    @ToastrHelper.ProcessToastrs()
```
## Using with GET-POST
You can use only Toastr Service and add your notification messages to it. For example;
```csharp
    ToastrService.AddToUserQueue(new Toastr(message : "Your message.", type: ToastrType.Success));
```
You can unlimited add message to queue. which stored from ToastrService with SessionId and will show to user with response.

**Sample POST Action(s);**

```csharp
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
    
            ToastrService.AddToUserQueue(new Toastr(message : "Customer added.", type: ToastrType.Success));
            
			//return RedirectToAction("Index");
        }
    
        return View(customer);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Customer customer)
    {
        if (ModelState.IsValid)
        {
            ToastrService.AddToUserQueue(new Toastr(message: "Customer updating.", type: ToastrType.Warning));
    
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
    
            ToastrService.AddToUserQueue(new Toastr(message: "Customer updated.", type: ToastrType.Success));
    
            return RedirectToAction("Index");
        }
        
        return View(customer);
    }
```
## Using with AJAX Response

You can use ToastrService.ToJavascript(...) method for JavaScriptResult and return js with response page. Toastrs will be shown.

```csharp
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateCategoryAjax(string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName))
        {
            ToastrService.AddToUserQueue("Category can not be empty.", "Requred !", ToastrType.Error);
        }
        else
        {
            db.Categories.Add(new Category { Name = categoryName });
            db.SaveChanges();
    
            ToastrService.AddToUserQueue("Category added.", "Success", ToastrType.Success);
        }
    
        string js = ToastrService.ToJavascript(ToastrService.ReadAndRemoveUserQueue());
    
        // <script></script> no needed..
        return JavaScript(js);
    }
```
