import * as ko from "knockout";
import { LogService } from "../../services/logservice";

class EditContentPane {

	constructor(params: any) {
		LogService.debug("EditContentPane, constructor");
		LogService.debug(params);
    }
}

export = EditContentPane;