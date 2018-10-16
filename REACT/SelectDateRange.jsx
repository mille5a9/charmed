class SelectDateRange extends Component {
    constructor(props) {
        super(props);
        this.state = {
            startval: this.props.startval,
            endval: this.props.endval
        };
    }

    handleChange(value, name) {
        this.props.onChange(value, name);
        this.setState({ [name]: value });
    }

    render() {
        var today = new Date();
        return <div>
            <label className="labelsize">From: </label>
            <input id="fromdate" className="text-right" type="date" value={this.state.startval} onChange={(e) => this.handleChange(e.target.value, 'startval')} max={today} />

            <label className="labelsize ml-5">To: </label>
            <input id="todate" className="text-right mr-5" type="date" value={this.state.endval} onChange={(e) => this.handleChange(e.target.value, 'endval')} max={today} />

            <button id="refreshbtn" className="btn btn-primary" onClick={() => this.props.onClick()}>Refresh</button>
        </div>
    }
}