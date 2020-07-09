import React from 'react';
import '../notebook/SaveNewNotebook.css';
const { fetchPersons } = require('../../util/HttpHelper')

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
        await this.fetchRooms()
        await this.fetchPersons()
    }

    
    fetchPersons = async () => {
        const url = "http://192.168.0.94:8016/person";
        const response = await fetch(url);
        const data = await response.json();
        console.log(data)
        this.setState({ personen: data });
    }

    fetchRooms = async () => {
        const url = "http://192.168.0.94:8015/material/notebook";
        const response = await fetch(url);
        const data = await response.json();
        console.log(data)
        this.setState({ rooms: data });
    }

    handlePersonChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedPerson = JSON.parse(value)
    
            this.setState({
                selectedPerson: value,
                person_id: selectedPerson.id,
            })
            console.log(selectedPerson.id)
        } else {
            this.setState({
                selectedPerson: '',
                person_id: ''
            })
        }
    }

    handleRoomChange = changeEvent => {
        this.setState({
            selectedRoom: changeEvent.target.value,
        })
        var selectedRoom = JSON.parse(changeEvent.target.value);
        this.setState({
            ...this.state.paramsRoom,
            id: selectedRoom.id,

        })
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
        var location = this.state.location_id === '' ? null : Number(this.state.location_id)
        
        const body = {
            serial_number: this.state.serial_number,
            make: this.state.make,
            model: this.state.model,
            person_id: person_id,
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
                selectedPerson: ''
            })

            console.log(body)
        } catch (error) {
            alert(error)
        }
    }

    render() {
        return (
            <div className="input-wrapper">
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
                <input className="input-field" value={this.state.location_id} name="location_id" onChange={(event) => this.updateWithEvent(event)} placeholder="Standort"></input>
                <div type="submit" className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
            </div>
        )
    }
}