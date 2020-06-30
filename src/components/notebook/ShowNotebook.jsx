import React from 'react';
const { fetchData } = require('../../util/HttpHelper')

export default class ShowNotebook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            serial_number: "",
            make: "",
            model: "",
            location_id: "",
            person_id: "",
        }
    }

    componentDidMount() {
        console.log(this.props.serial_number);
        fetchData(this.props.serial_number).then(data => 
            this.setState(
            {
                serial_number: data[0].serial_number,
                make: data[0].make,
                model: data[0].model,
                location_id: data[0].location_id,
                person_id: data[0].person_id
            }));
    }



    render() {
        return (
            <>
            <div>{this.state.serial_number}</div>
            <div>{this.state.make}</div>
            <div>{this.state.model}</div>
            <div>{this.state.location_id}</div>
            <div>{this.state.person_id}</div>
            </>
        )
    }
}