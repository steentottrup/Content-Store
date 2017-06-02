import * as ko from "knockout";

abstract class Page {
	protected currentTemplate: KnockoutObservable<string> = ko.observable<string>();

	constructor(template: string) {
		this.currentTemplate(template);
	}

	public get template(): string {
		return this.currentTemplate();
	}
}

export default Page;
