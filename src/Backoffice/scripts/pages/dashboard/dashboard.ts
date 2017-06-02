import * as ko from "knockout";
import Application from "app";
import Page from "page";

class DashboardPage {
	private app: Application;
	private currentPage: KnockoutObservable<Page>;

    constructor(params: any) {
		this.app = Application.getInstance();
    }

    //search = () => {
    //    this.results.removeAll();
    //    if (this.queryField().length > 2) {
    //        UserService.search(this.queryField())
    //            .then((result) => {
    //                result.forEach((r) => {
    //                    this.results.push(new RelicProfile(r));
    //                });
    //            });
    //    }
    //    else {
    //        // TODO: Display error message
    //    }
    //};

    //selectProfile = (h: Dtos.IProfile, event: any) => {
    //    this.app.navigateTo("/coh2/profile/" + h.profileId.toString(), event);
    //};

}

export = DashboardPage;