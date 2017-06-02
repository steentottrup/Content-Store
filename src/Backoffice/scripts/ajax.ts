import * as P from "promise";

export module Ajax {
    var defer = P.P.defer;
    var when = P.P.when;
    interface Promise<Value> extends P.P.Promise<Value> { }

    export interface IResponse<Value> {
        StatusCode: number;
        StatusText: string;
        Body: string;
        Data: Value;
    }

    class Response<Value> implements IResponse<Value> {
        StatusCode: number;
        StatusText: string;
        Body: string;
        Data: Value;
    }

    enum HttpMethod {
        POST,
        PUT,
        DELETE,
        GET
    }

    function unpackReponse<Value>(request: XMLHttpRequest): IResponse<Value> {
        var output = new Response<Value>();
        output.Body = request.responseText;
        output.StatusCode = request.status;
        output.StatusText = request.statusText;
        if (request.responseText.length > 1) {
            output.Data = <Value>JSON.parse(request.responseText);
        }

        // TODO: More here ???

        return output;
    }

    function readCookie(name: string) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1, c.length);
            }
            if (c.indexOf(nameEQ) === 0) {
                return c.substring(nameEQ.length, c.length);
            }
        }
        return null;
    }

    function send<Value>(url: string, data: any, method: HttpMethod): Promise<Response<Value>> {
        var d = defer<Response<Value>>();

        var jsXHR = new XMLHttpRequest();
        jsXHR.open(HttpMethod[method], url);
        jsXHR.setRequestHeader("Accept", "application/json");
        jsXHR.setRequestHeader("Content-Type", "application/json");
        // Antiforgery magic!
        jsXHR.setRequestHeader("X-XSRF-TOKEN", readCookie("XSRF-TOKEN"));

        //if (headers != null)
        //	headers.forEach(header =>
        //		jsXHR.setRequestHeader(header.header, header.data));

        jsXHR.onload = (ev) => {
            if (jsXHR.status < 200 || jsXHR.status >= 300) {
                d.reject({ message: jsXHR.statusText });
            }
            else {
                d.resolve(unpackReponse<Value>(jsXHR));
            }
        }
        jsXHR.onerror = (ev) => {
            d.reject({ message: "Error " + HttpMethod[method] + "ing data to url '" + url + "', check that it exists and is accessible" });
        };

        //if (method == HttpMethod.POST || method == HttpMethod.PUT) {
        //	jsXHR.send(data);
        //}
        //else {
        //	jsXHR.send();
        //}
        jsXHR.send(data);

        return d.promise();
    }

    export function put<Value>(url: string, data: any): Promise<Response<Value>> {
        return send<Value>(url, JSON.stringify(data), HttpMethod.PUT);
    }
    export function post<Value>(url: string, data: any): Promise<Response<Value>> {
        return send<Value>(url, JSON.stringify(data), HttpMethod.POST);
    }
    export function get<Value>(url: string, data: any): Promise<Response<Value>> {
        return send<Value>(url, data, HttpMethod.GET);
    }
    export function del<Value>(url: string): Promise<Response<Value>> {
        return send<Value>(url, "", HttpMethod.DELETE);
    }
}