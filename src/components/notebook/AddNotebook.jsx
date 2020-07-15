import React from 'react';
import '../notebook/SaveNewNotebook.css';
const { fetchPersons, fetchRooms } = require('../../util/HttpHelper')

export default class AddNotebook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            serial_number: '',
            make: '',
            model: '',
            location_id: '',
            person_id: '',
            personen: [],
            params: { id: null },
            selectedPerson: '',
            rooms: [],
            paramsRoom: { id: null },
            selectedRoom: '',
            personId: ''
        }
    }

    async componentDidMount() {
        this.fetchPersons()
        this.fetchRooms()
    }


    fetchPersons = async () => {
        const persons = await fetchPersons()
        this.setState({ personen: persons });
    }

    fetchRooms = async () => {
        const rooms = await fetchRooms()
        this.setState({ rooms: rooms });
    }

    handlePersonChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedPerson = JSON.parse(value)

            this.setState({
                selectedPerson: value,
                person_id: selectedPerson.id,
            })
        } else {
            this.setState({
                selectedPerson: '',
                person_id: ''
            })
        }
    }

    handleRoomChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedRoom = JSON.parse(value)

            this.setState({
                selectedRoom: value,
                location_id: selectedRoom.id,
            })
            console.log(selectedRoom)
        } else {
            this.setState({
                selectedRoom: '',
                location_id: ''
            })
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
        var person_id = this.state.person_id === '' ? null : Number(this.state.person_id)
        var location_id = this.state.location_id === '' ? null : Number(this.state.location_id)

        const body = {
            serial_number: this.state.serial_number,
            make: this.state.make,
            model: this.state.model,
            person_id: person_id,
            location_id: location_id
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

            let result = await fetch('http://192.168.0.94:8015/material/notebook', req)
            const r = await result.text()
            if (r !== "")
                alert(r)

            this.props.fetchNotebooks()

            this.setState({
                serial_number: '',
                make: '',
                model: '',
                location_id: '',
                person_id: '',
                selectedPerson: '',
                selectedRoom: ''
            })

            console.log(body)
        } catch (error) {
            alert(error)
        }
    }

    render() {
        return (
            <div className="input-wrapper">
<div>
                <div className="head-text">Neues Notebook</div>
                <input className="input-field" value={this.state.make} required name="make" onChange={(event) => this.updateWithEvent(event)} placeholder="Marke"></input>
                <input className="input-field" value={this.state.model} name="model" onChange={(event) => this.updateWithEvent(event)} placeholder="Modell"></input>
                <input className="input-field" value={this.state.serial_number} name="serial_number" onChange={(event) => this.updateWithEvent(event)} placeholder="Seriennummer"></input>

                <select className="input-field-dropdown" value={this.state.selectedPerson} onChange={this.handlePersonChange}>
                    <option value="" defaultValue >Person auswählen</option>
                    {this.state.personen.map((personen, key) => {
                        return <option key={key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
                    })}
                </select>

                <select className="input-field-dropdown" value={this.state.selectedRoom} onChange={this.handleRoomChange}>
                    <option value="" defaultValue >Raum auswählen</option>
                    {this.state.rooms.map((rooms, key) => {
                        var x = rooms.adresslocations[0] != undefined ? " / " + rooms.adresslocations[0].address.place : ""
                        return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + " / " + x}</option>
                    })}
                </select>
</div>
                <div type="submit" className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
            </div>
        )
    }
}