import React from 'react';
const { fetchData } = require('../../util/HttpHelper')

export default class ShowNotebook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            serialNumber: undefined,
            make: undefined,
            model: undefined,
            location: undefined,
            person: undefined,
        }
    }

    componentDidMount() {
        console.log(this.props.serielNumber);
        fetchData(this.props.serialNumber).then(data => this.setState(
            {
                serialNumber: data.request.serialNumber,
            }));
    }



    render() {
        return (
            <>
            <div>{this.state.serielNumber}</div>
            </>
        )
    }
}