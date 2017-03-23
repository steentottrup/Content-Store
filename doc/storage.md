# Content Storage

Content Store is all about storing content.

Your content might be very dynamic, and only share a few fields across documents, or it could be a handful of different types of content with many common fields and thousands of documents.

With Content Store you can put your content in different containers. It could be containers for:

- Users
- Files
- Documents

You can also just put everything in a single container, but that would probably not be a good idea.

It's easy to create a new container, and containers perform much better with fewer indexes, so keeping content with shared fields in the same container makes sense for several reasons.

Content Store uses a document database for storing content. This kind of database is also often referred to as a NoSql database. This ensures Content Store can scale to handle thousands and millions of pieces of content.

## Containers

Content is stored in containers.

In the configuration of a container, you decide on what content types are allowed in the container. That way content does not end up in the wrong container, because of some error in the application calling the Content Store Api.

If you're building a regular website, common containers are files, users, pages and maybe documents.

Content in containers are stored without any kind of structure. So if you fetch all content from a container, you get a long list of elements.

Your application can of course use some of the fields of the content elements to hold relations between them.

An example of how to use fields to create structure where there is none, is the trees of the Content Store web UI. To read more about this, go to the documentation for the web UI trees.
