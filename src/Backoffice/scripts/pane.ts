
export default class Pane {
    public title: string;
    public template: string;
    public parameters: any;

    constructor(title: string, template: string, parameters: any) {
        this.title = title;
        this.template = template;
        this.parameters = parameters;
    }
}
