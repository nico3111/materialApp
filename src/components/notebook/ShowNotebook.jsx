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
        fetchData(this.props.id).then(data => this.setState(
            {
                serial_number: data.serial_number,
                make: data.make,
                model: data.model,
                location_id: data.location_id,
                person_id: data.person_id
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