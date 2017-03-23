# Trees
The trees of the Content Store web UI is a way of adding some structure to the flat list of content elements in a given container.

The Content Store web UI will include several types of trees, common for all is that they use one or more fields of the contained content elements to create a tree structure.

It is not required to have a tree for each container. 

A container can hold content elements that will never show up in a tree. If one or more of the fields used to create the tree structure is missing from a content element (or the content type of an element), it might never be visible.

Planned tree types for the Content Store web UI are:

- "File system", a structure with "folder" types and "file" types, where "folder" and "file" types hold the id of their parent "folder". The sort order between tree node on the same level will be alphabetical.
- "Site structure", a structure with "page" types that holds the id of a parent "page" type. The sort order between tree node on the same level will be dictated by a field on the content types used for the "page" elements.
- "Taxanomy", a structure where one or more fields are used to create the structure. For more details on this, look at the definition of taxanomy.

The configuration of the tree contains a definition for the structure, based on the content types, so you can define, and enforce, which content types are allowed as "children" of which content types.

Common for all trees is that contained content types can be shown or hidden. This allows for hiding certain content types that might count in the thousands or millions.

Let's say you need to have a news archive, and you have thousands of news items in a flat structure below the page node that is your news archive page on your website. It would probably not make a lot of sense scrolling through thousands of nodes in your tree to locate a certain news item anyway. So we hide them. Instead you can get a search/filter view in the web UI instead, helping you locate the element you need.

## Examples

### File system type

At the root level you can create content elements with the content type "userFolder", and below content with the "userFolder" type you can create "user" or "userFolder" content elements.

```
{
  "name": "users",
  "sorting": {
    "type": "alphabetical",
    "fieldName": "csName"
  },
  "contentTypes": [
    {
      "name": "user",
      "allowedChildren": [ ],
      "visibleInTree": true
    },
    {
      "name": "userFolder",
      "allowedChildren": [ "user", "userFolder" ],
      "visibleInTree": true
    }
  ],
  "root": "userFolder"
}
```

### Site structure type

At the root level you can create content elements with the "page" content type, and below content with the "page"  type you can create "page" or "listItem" content elements.

Content with the "listItem" content type is not visible in the tree.

Nodes on the same level are sorted using the "csSortOrder" field of the content elements.

```
{
  "name": "pages",
  "sorting": {
    "type": "sortOrder",
    "fieldName": "csSortOrder"
  },
  "templates": [
    {
      "name": "page",
      "allowedChildren": [ "page", "listItem" ],
      "visibleInTree": true
    },
    {
      "name": "listItem",
      "allowedChildren": [ ],
      "visibleInTree": false
    }
  ],
  "root": "page"
}
```
