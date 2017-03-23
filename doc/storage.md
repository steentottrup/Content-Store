### Content Storage

Content Store is all about storing content.

Your content might be very dynamic, and only share a few fields across documents, or it could be a handful of different types of content with many common fields and thousands of documents.

With Content Store you can put your content in different containers. It could be containers for:

- Users
- Files
- Documents

You can also just put everything in a single container, but that would probably not be a good idea.

It's easy to create a new container, and containers perform much better with fewer indexes, so keeping content with shared fields in the same container makes sense for several reasons.

Content Store uses a document database for storing content. This kind of database is also often referred to as a NoSql database. This makes sure Content Store can scale to handle thousands and millions of pieces of content.

