# Content Types
The Content Type configuration contains the schema telling Content Store what fields a content element with a given content type can be expected to hold.

You can select a new content type for content, and the schema of the content type can be changed, so you can have content in the database that does not have the expected fields, or does not validate against the requirements of the selected content type.

This is expected and a valid state.

## Inheritance
A content type can inherit fields from other content types, a content type, can in fact, inherit from several other content types. The ony limit that this option holds, is that you can not create a circular reference, meaning this senario is not allowed: content type 1 inherits from content type 2, which again inherits from content type 1.

This allows for any number of simple, small, and very specific content types, that can be used and inherited to express complex rules for your content.

The importance of the inherited fields are dictate by the order of the content types in the parentTypes field, meaning fields from the firste content type mentioned in the field, will overwrite fields with the same name in any other content type in the list of parent types or any content type they inherit from.

## Data types
Content Store has build-in support for these simple field types:
- Boolean
- Date
- DateTime
- Decimal
- EncryptedString
- Id
- Integer
- String
- Time

And these complex types:
- Array
- Object

Common for all field types are that the values can be null or more correctly put, not set.

There are no plans for adding more data types or making it possible to plug in new types, but this part of the core will be very pluggable by default.

### Boolean
Can be true or false

### Date
A date, with no time element.

### DateTime
A date and time, stored as UTC.

### Decimal
A decimal number.

### EncryptedString
Like the string data type, this can contain any number of characters/letters/etc. but unlike the regular string, this is an encrypted string. This means that if communication between the server and database is intercepted (not a likely senario), or if the communication between your application and Content Store is intercepted (a more likely senario), the data can not easily be used for anything.

With the default configuration, the Content Store Api will not return the value of an encrypted string, unless the call is made using HTTPS (which all calls should be anyway!) and it is made to explicitly get the string decrypted. The string will not be included in the result of a regular Api call for the fields of a content element.

### Id
This is a unique indentifier.

Data is not stored in a relationel database, but this data type can still be used to create a relationship between two content elements. An example would be the author field of a content element, which is a reference to another content element (an user element).

### Integer
A (64 bit) number.

### String
Any numbers of characters/letters/etc. Strings are stored with the UTF-8 encoding.

### Time
A time, with no date element.

### Array
An array, containing elements of any of the build-in types. Yes, you can have arrays in arrays.

### Object
A complex type, described by its own content type.

## Examples

### Simple content type with 2 fields

```
{
  "name": "node",
  "parentTypes": [],
  "fields": [
    {
      "name": "Name",
      "dataType": "string",
      "editable": true,
      "requirements": [
        {
          "type": "required"
        }
      ]
    },
    {
      "name": "Title",
      "dataType": "string",
      "editable": false,
      "requirements": [
        {
          "type": "required"
        }
      ]
    }
  ]
}
```

### Content type inheritance

```
{
  "name": "template1",
  "parentTypes": ["template2"],
  "fields": [
    {
      "name": "Name",
      "dataType": "string",
      "editable": true,
      "requirements": [
        {
          "type": "required"
        }
      ]
    }
  ]
}
```

```
{
  "name": "template2",
  "parentTypes": [],
  "fields": [
    {
      "name": "Name",
      "dataType": "string",
      "editable": false,
      "requirements": [
        {
          "type": "required"
        }
      ]
    }
  ]
}
```

This will give you a template1 with one field, Name, that is editable, as the Name field from template2 is overwritten by the field with the same name from template1.

