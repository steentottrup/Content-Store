# Content Store Api

The Content Store solution, being a headless/decoupled content management system, has a powerful Api to access the content it stores. The solution actually includes 2 Apis, one is commonly referred to as the Api, the Content Store Api, the other is the Content Store Back Office Api.

## Content Store Api
The Api you should use to create, read, update and delete content in your Content Store solution.

## Content Store Back Office Api
This Api is exclusively to be used by the Content Store web UI.

In the future it might be possible to extend on the web UI, and that would be the only valid reason for using this Api from any code that is not part of the core project.

Generally, stay away from calling this Api!