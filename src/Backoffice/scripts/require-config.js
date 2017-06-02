require.config({
	baseUrl: "/scripts",
	paths: {
		"knockout": "lib/knockout-latest",
		"promise": "promise",
		"text": "lib/text",
		"page": "page",
		"ajax": "ajax",
		"pane": "pane"
		// *** Service
	},
	urlArgs: "bust=" + (new Date()).getTime()
});
require(["app"]);
