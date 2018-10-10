class TextInput extends Component {
    constructor(props) {
        super(props);

        this.state = {
            value: this.props.value
        };
        this.onChange = this.onChange.bind(this);
    }
    onChange(event) {
        if (this.props.format === 'percent') {
            var smalldecimalflag = false;

            if (event.target.value[1] === ".") {
                smalldecimalflag = true;
            }

            var input = parseFloat(event.target.value);
            if (smalldecimalflag ? (input > 10) : (input > 100)) {
                for (var exp = 1; smalldecimalflag ? input > 10 : input > 100; exp++) input = input / 10;
                input = Number.parseFloat(input).toPrecision(exp + 1);
                this.setState({ value: input });
                this.props.onChange(input);
                return;
            }
            if (!isNaN(input) || smalldecimalflag) {
                this.setState({ value: smalldecimalflag ? event.target.input : input });
            }
            else this.setState({ value: "" });
            this.props.onChange(input);
        }
        if (this.props.format === 'string') {
            this.setState({ value: event.target.value });
            this.props.onChange(event.target.value);
        }
    }
    render() {
        return <input
            type={this.props.type}
            className={this.props.className}
            value={this.state.value}
            onChange={this.onChange}
        />;
    }
}