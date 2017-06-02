import { LogService } from "../services/logservice";

export interface IRouteExecute {
    (values: IRouteValues): void;
}

interface IRouteConfig {
    path: string;
    onRouteTo: IRouteExecute;
}

export interface IRouteValues {
    [name: string]: string;
}

export class Route {
    private routeConfig: IRouteConfig;
    private routeParts: Array<string>;
    private routeArguments: Array<string>;
    private routeOptional: Array<string>;

    /* creates a route
     * @param path Path for the Route use {id} to indicate id is required, :id: to indicate id is optional
     * @param onRouteTo Function to Execute to start the Route
     */
    constructor(path: string, onRouteTo: IRouteExecute) {
        this.routeConfig = { path: path, onRouteTo: onRouteTo };
        this.routeParts = path.split("/");
        this.routeArguments = [];
        this.routeOptional = [];
        // find all Mandatory Parts (delimited with { and })
        this.routeParts.forEach((part: string): void => {
            if (part.charAt(0) === "{" && part.charAt(part.length - 1) === "}") {
                this.routeArguments.push(part.substr(1, part.length - 2));
            }
        }, this);
        // find all Options Parts (delimited with : and 🙂
        this.routeParts.forEach((part: string): void => {
            if (part.charAt(0) === ":" && part.charAt(part.length - 1) === ":") {
                this.routeOptional.push(part.substr(1, part.length - 2));
            }
        }, this);
    }
    /* checks to see if this Route matches the provided path
     * @param path Path to Validate for a match
     */
    matches(path: string): boolean {
        try {
            var incomingParts: Array<string> = path.split("/");
            if (incomingParts.length < this.routeParts.length - this.routeOptional.length) {
                return false;
            }
            for (var i: number = 0; i < incomingParts.length; i++) {
                var incoming: string = incomingParts[i];
                var part: string = this.routeParts[i];
                if (typeof part === "undefined") {
                    // route is too long (exceeded array)
                    return false;
                }
                if (part.charAt(0) !== "{" && part.charAt(0) !== ":" &&
                    part.charAt(part.length - 1) !== "}" && part.charAt(part.length - 1) !== ":") {
                    // this is not a parameter, the route must match
                    if (part.substr(1, part.length - 2) !== incoming.substr(1, incoming.length - 2)) {
                        return false;
                    }
                }
            }
            return true;
        }
        catch (ex
        ) {
            if (console.error) {
                console.error(ex);
            }
            return false;
        }
    }
    /* returns an IRouteValues of the parameters which were found in the path
     * may return nothing if no paramters are in the route
     */
    getArguments(path: string): IRouteValues {
        var args: IRouteValues = {};
        var incomingParts: Array<string> = path.split("/");
        for (var i: number = 0; i < incomingParts.length; i++) {
            var incoming: string = incomingParts[i];
            var part: string = this.routeParts[i];
            if ((part.charAt(0) === "{" || part.charAt(0) === ":") &&
                (part.charAt(part.length - 1) === "}" || part.charAt(part.length - 1) === ":")) {
                // this is not a parameter, the route must match
                args[part.substr(1, part.length - 2)] = incoming;
            }
        }
        return args;
    }
    fn: IRouteExecute = (values: IRouteValues): void => {
        this.routeConfig.onRouteTo(values);
    };
}

export class Router {
    private siteBase: string;
    private appBase: string;
    private routes: Array<Route>;
    /* creates a Router
     * siteBase Base URL for everything (eg. /)
     * appBase URL for where the application runs inside an MVC Route (eg. /App)
     */
    constructor(siteBase: string, appBase: string) {
        this.siteBase = siteBase;
        this.appBase = appBase;
        this.routes = [];
    }
    /* registers a Route with the Router */
    registerRoute(route: Route): void {
        this.routes.push(route);
    }
    /* gets a Consistent Route Path 
     * provides Original Route for HTML4 browsers
     */
    getCurrentPath(): string {
        // html4 - honor Navigation if Provided before redirect
        var originalRoute: string = window.sessionStorage.getItem("html4.route");
        if (originalRoute) {
            window.sessionStorage.setItem("html4.route", null);
            return originalRoute;
        }
        return window.location.pathname;
    }
    clicked(url: string, event: any) {
        var router: Router = this;
        if (url.substr(0, router.appBase.length) === router.appBase) {
            event.preventDefault();
            router.navigateTo(url);
        }
    }
    /* starts the Router (the application does this itself, not intended to be called) 
     * if True is Returned, The application should navigate to the getCurrentPath() value
     * if False is returned, we will be redirecting HTML4 browsers, so omit
     */
    start(): boolean {
        var currentURL: string = window.location.pathname;
        // if Not supported, note the path, then redirect to base
        var router: Router = this;
        window.addEventListener("popstate", (ev: PopStateEvent): void => {
            LogService.debug("router: popstate fired");
            var appRoute: string = window.location.pathname.substr(router.appBase.length);
            for (var i: number = 0; i < router.routes.length; i++) {
                var route: Route = router.routes[i];
                if (route.matches(appRoute)) {
                    route.fn(route.getArguments(appRoute));
                    LogService.debug("router: found matching route");
                    break;
                }
            }
        });
        return true;
    }
    /* builds a Url from a Relative URL string (eg. api controller calls) */
    buildURL(relativeUrl: string): string {
        if (relativeUrl.charAt(0) === "/") {
            return this.siteBase + relativeUrl.substr(1);
        }
        return this.siteBase + relativeUrl;
    }

    /* navigates to a Route based on the string Provided */
    navigateTo(fullPath: string): boolean {
        // make sure it's an app URL
        if (fullPath.substr(0, this.appBase.length) === this.appBase) {
            var appRoute: string = fullPath.substr(this.appBase.length);
            // find the Route
            for (var i: number = 0; i < this.routes.length; i++) {
                var route: Route = this.routes[i];
                if (route.matches(appRoute)) {
                    // execute this Matching Route
                    route.fn(route.getArguments(appRoute));
                    window.history.pushState(null, null, fullPath);
                    return true;
                }
            }
        }
        // no route was found - force user to backup
        if (console.error) {
            console.error("Invalid Route Detected trying to get to: " + fullPath);
        }
        return false;
    }
}
