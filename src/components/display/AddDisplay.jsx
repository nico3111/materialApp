import React from 'react';

export default class AddDisplay extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            serial_number: '',
            make: '',
            model: '',
            location_id: ''
        }
    }

    updateWithEvent(event) {
        const key = event.target.name;
        const value = event.target.value;

        this.setState({
            [key]: value
        })
    }

    async postData() {

        var location = this.state.location_id === '' ? null : Number(this.state.location_id)

        const body = {
            serial_number: this.state.serial_number,
            make: this.state.make,
            model: this.state.model,
            location_id: location
        }

        try {
            var req = {
                method: 'post',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(body)
            }

            let result = await fetch('http://192.168.0.94:8015/material/display', req)

            this.props.fetchDisplays()

            this.setState({
                serial_number: '',
                make: '',
                model: '',
                location_id: ''
            })

            console.log(body)
        } catch (error) {
            console.log(error)
        }
    }

    render() {
        return (
            <div>
                <div className="input-wrapper">
                    <div className="head-text">Neuer Bildschirm</div>
                    <input className="input-field" value={this.state.make} name="make" onChange={(event) => this.updateWithEvent(event)} placeholder="Marke"></input>
                    <input className="input-field" value={this.state.model} name="model" onChange={(event) => this.updateWithEvent(event)} placeholder="Modell"></input>
                    <input className="input-field" value={this.state.serial_number} name="serial_number" onChange={(event) => this.updateWithEvent(event)} placeholder="Seriennummer"></input>
                    <input className="input-field" value={this.state.location_id} name="location_id" onChange={(event) => this.updateWithEvent(event)} placeholder="Standort"></input>
                    <div className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
                </div>
            </div>
        )
    }
}