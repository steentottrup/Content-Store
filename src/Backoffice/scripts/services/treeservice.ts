import * as P from "promise";
import { Ajax } from "ajax";

export module TreeService {

    export function findTrees(): P.P.Promise<Dtos.ITree[]> {
        return Ajax.get<Dtos.ITree[]>("/api/backoffice/tree", {})
            .then((result) => {
                return result.Data;
            });
    }

	export function findRootNodes(type: string): P.P.Promise<Dtos.ITreeNode[]> {
		return Ajax.get<Dtos.ITreeNode[]>("/api/backoffice/tree/" + type, {})
			.then((result) => {
				return result.Data;
			});
	}
}
