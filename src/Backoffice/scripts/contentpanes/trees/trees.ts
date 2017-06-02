import * as ko from "knockout";
import { TreeService } from "../../services/treeservice";
import Application from "app";
import Pane from "pane";

class TreePane {
	private trees: KnockoutObservableArray<Dtos.ITree> = ko.observableArray<Dtos.ITree>();
	private app: Application;

	constructor(params: any) {
		this.app = Application.getInstance();

		TreeService.findTrees().then((result) => {
			result.forEach((tree) => {
				this.trees.push(tree);
			});
		});
	}

	showTree = (t: Dtos.ITree, event: any) => {
		this.app.navigateTo("/admin/dashboard/" + t.id, event);
	};
}

export = TreePane;