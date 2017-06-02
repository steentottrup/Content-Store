import * as ko from "knockout";
import * as Routing from "../scripts/routing/router";
import Page from "../scripts/page";
import DashboardPage from "../scripts/dashboardpage";
import LoginPage from "../scripts/loginpage";

// *** TODO *** Pages, move somewhere else!!

ko.components.register("dashboard", {
	viewModel: { require: "pages/dashboard/dashboard" },
	template: { require: "text!pages/dashboard/dashboard.html" }
});

ko.components.register("login", {
	viewModel: { require: "pages/login/login" },
	template: { require: "text!pages/login/login.html" }
});

// *** TODO *** Content Panes, move somewhere else!!

ko.components.register("trees", {
	viewModel: { require: "contentpanes/trees/trees" },
	template: { require: "text!contentpanes/trees/trees.html" }
});

ko.components.register("tree", {
	viewModel: { require: "contentpanes/trees/tree" },
	template: { require: "text!contentpanes/trees/tree.html" }
});

ko.components.register("editcontent", {
	viewModel: { require: "contentpanes/editcontent/editcontent" },
	template: { require: "text!contentpanes/editcontent/editcontent.html" }
});

export default class Application {
	private static _instance: Application = new Application();

	private router: Routing.Router = new Routing.Router("", "");
	private currentPage: KnockoutObservable<Page> = ko.observable<Page>();

	public get page(): KnockoutObservable<Page> {
		return this.currentPage;
	}

	private constructor() {
		if (Application._instance) {
			throw new Error("Error: Instantiation failed: Use Application.getInstance() instead of new.");
		}
		Application._instance = this;
		this.init();

		this.router.start();

		var currentURL: string = window.location.pathname;
		this.router.navigateTo(currentURL);
	}

	private init = () => {
		this.router.registerRoute(
			new Routing.Route("/admin/",
				(values: Routing.IRouteValues): void => {
					this.switchPage("login");
				}));
		this.router.registerRoute(
			new Routing.Route("/admin/dashboard",
				(values: Routing.IRouteValues): void => {
					this.switchPage("dashboard");
				}));
		this.router.registerRoute(
			new Routing.Route("/admin/dashboard/{tree}",
				(values: Routing.IRouteValues): void => {
					this.switchTree(values["tree"]);
				}));
		this.router.registerRoute(
			new Routing.Route("/admin/dashboard/{tree}/{id}",
				(values: Routing.IRouteValues): void => {
					this.switchContentEditor(values["tree"], values["id"]);
				}));
	};

	private switchToDashboard() : void {
		this.switchPage("dashboard");
	}

	private switchPage(template: string) {
		switch (template) {
			case "dashboard":
				this.currentPage(new DashboardPage(template));
				break;
			case "login":
				this.currentPage(new LoginPage(template));
				break;
			default:
				// TODO:
		}
	}

	private switchTree(tree: string) {
		if (!(this.currentPage() instanceof DashboardPage)) {
			this.switchToDashboard();
		}

		var dashboardPage;
		dashboardPage = this.currentPage() as DashboardPage;
		dashboardPage.switchTree(tree);
	}

	private switchContentEditor(tree: string, id: string) {
		this.switchTree(tree);

		var dashboardPage;
		dashboardPage = this.currentPage() as DashboardPage;
		dashboardPage.switchContentEditor(tree, id);
	}

	public navigateTo(url: string, event: any) {
		this.router.clicked(url, event);
	}

	public static getInstance(): Application {
		return Application._instance;
	}
}

ko.applyBindings(Application.getInstance());
