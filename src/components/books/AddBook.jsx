import React from 'react';
const { fetchPersons, fetchRooms } = require('../../util/HttpHelper');

export default class AddDisplay extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            title: '',
            isbn: '',
            quantity: '',
            person_id: '',
            location_id: '',
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
            console.log(value)
            console.log(selectedPerson)

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

        var person = this.state.person_id === '' ? null : Number(this.state.person_id)
        var location = this.state.location_id === '' ? null : Number(this.state.location_id)

        const body = {
            title: this.state.title,
            isbn: this.state.isbn,
            quantity: this.state.quantity,
            person_id: person,
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

            let result = await fetch('http://192.168.0.94:8015/material/book', req)
            const r = await result.text()
            if (r !== "")
                alert(r)

            this.props.fetchBooks()

            this.setState({
                title: '',
                isbn: '',
                person_id: '',
                location_id: '',
                selectedPerson: ''
            })

            console.log(body)
        } catch (error) {
            console.log(error)
        }
    }

    render() {
        return (
            <div className="input-wrapper">
                <div className="head-text">Neues Buch</div>
                <input className="input-field" value={this.state.title} name="title" onChange={(event) => this.updateWithEvent(event)} placeholder="Titel"></input>
                <input className="input-field" value={this.state.isbn} name="isbn" onChange={(event) => this.updateWithEvent(event)} placeholder="ISBN"></input>
                <input type="number" min="1" className="input-field" value={this.state.quantity} name="quantity" onChange={(event) => this.updateWithEvent(event)} placeholder="Anzahl"></input>
                
                <select className="input-field-dropdown" value={this.state.selectedPerson} onChange={this.handlePersonChange}>
                    <option value="" disabled defaultValue hidden>Person auswählen</option>
                    {this.state.personen.map((personen, key) => {
                        return <option key={key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
                    })}
                </select>

                <select className="input-field-dropdown" value={this.state.selectedRoom} onChange={this.handleRoomChange}>
                    <option value="" defaultValue >Raum auswählen</option>
                    {this.state.rooms.map((rooms, key) => {
                         var x = rooms.adresslocations[0] != undefined ? " / " + rooms.adresslocations[0].address.place : ""
                        return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + x}</option>
                    })}
                </select>

                <div className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
            </div>
        )
    }
}