import * as ko from "knockout";
import Page from "page";

export default class DashboardPage extends Page {
	private currentTree: KnockoutObservable<string> = ko.observable<string>();
	private currentContentEditor: KnockoutObservable<string> = ko.observable<string>();

	constructor(template: string) {
		super(template);
	}

	public get tree(): KnockoutObservable<string> {
		return this.currentTree;
	}

	public get contentEditor(): string {
		return this.currentContentEditor();
	}

	public switchTree = (tree: string) => {
		if (this.currentTree() !== tree) {
			this.currentTree(tree);
		}
	};

	public switchContentEditor = (tree: string, id: string) => {
		this.switchTree(tree);
		if (this.currentContentEditor() !== id) {
			this.currentContentEditor(id);
		}
	};
}