# Content Store Api

The Content Store solution, being a headless/decoupled content management system, has a powerful Api to access the content it stores. The solution actually includes 2 Apis, one is commonly referred to as the Api, the Content Store Api, the other is the Content Store Back Office Api.

## Content Store Api
The Api you should use to create, read, update and delete content in your Content Store solution.

### Managing content using the Api
If you are using trees to structure your content elements (you probably should!), you access the content elements through the tree the content belongs to.
This will ensure that the user of your application has the permissions needed to read the content, and will enforce any rules given in the configuration of the tree, this includes but is not limited to: fields required by the content type, fields required by the tree, etc.
See the tree documentation for further information.

#### Getting a content element
To get a given content element, you make a simple HTTPS GET request to this url:

```
https://<siteurl>/api/tree/<treename>/<id>
```

#### Creating a content element
The actual call to the Api to create a new content element is simple, you just need to make a HTTPS POST request to this url:

```
https://<siteurl>/api/tree/<treename>
```

The content of the actual request is a bit more complex, and depend on the type of the content you are trying to create.

(TODO)

#### Updating a content element
The actual call to the Api to update a content element is simple, you just need to make a HTTPS PUT request to this url:

```
https://<siteurl>/api/tree/<treename>/<id>
```

(TODO)

#### Deleting a content element

(TODO)

#### Getting content without using trees
All content in Content Store is place in a container. To get a given content element, you need to know the name of this container and the Id of the element. To get the content, you need to make a simple HTTPS GET request to this url:

```
https://<siteurl>/api/content/<containername>/<id>
```
Requesting content this way, will ignore any rules given in the tree the content might also exist in, including permissions. This type of call do require a special permission.

## Content Store Back Office Api
This Api is exclusively to be used by the Content Store web UI.

In the future it might be possible to extend on the web UI, and that would be the only valid reason for using this Api from any code that is not part of the core projects.

Generally, stay away from calling this Api!
