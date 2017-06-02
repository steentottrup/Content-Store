import * as ko from "knockout";
import Application from "app";
import DashboardPage from "../../dashboardpage";
import { LogService } from "../../services/logservice";
import { TreeService } from "../../services/treeservice";

class TreePane {
	private tree: KnockoutObservable<string>;
	private nodes: KnockoutObservableArray<Dtos.ITreeNode> = ko.observableArray<Dtos.ITreeNode>();

	constructor(params: any) {
		LogService.debug("TreePane, constructor");
		LogService.debug(params);

		var dashboardPage: KnockoutObservable<DashboardPage>;
		dashboardPage = Application.getInstance().page as KnockoutObservable<DashboardPage>;
		this.tree = dashboardPage().tree;

		TreeService.findRootNodes(params.id)
			.then((nodes) => {
				nodes.forEach((node) => {
					this.nodes.push(node);
				});
			});
	}

	showEditor = (n: Dtos.ITreeNode, event: any) => {
		Application.getInstance().navigateTo("/admin/dashboard/" + this.tree() + "/" + n.id, event);
	}
}

export = TreePane;