import React from 'react';
const { fetchData } = require('../../util/HttpHelper')

export default class ShowNotebook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            serial_number: undefined,
            make: undefined,
            model: undefined,
            location_id: undefined,
            person_id: undefined,
        }
    }

    componentDidMount() {
        console.log(this.props.serial_number);
        fetchData(this.props.serial_number).then(data => this.setState(
            {
                serial_number: data.request.serial_number,
            }));
    }



    render() {
        return (
            <>
            <div>{this.state.serial_number}</div>
            </>
        )
    }
}