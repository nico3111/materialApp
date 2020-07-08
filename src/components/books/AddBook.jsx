import React from 'react';

export default class AddDisplay extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            title: '',
            isbn: '',
            person_id: '',
            location_id: '',
            personen: [],
            params: { id: null },
            selectedPerson: '',
            rooms: [],
            paramsRoom: { id: null },
            selectedRoom: ''
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
        this.setState({
            selectedPerson: changeEvent.target.value,
        })
        var selectedPerson = JSON.parse(changeEvent.target.value);
        this.setState({
            ...this.state.params,
            id: selectedPerson.id,

        })
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

        var person = this.state.id === '' ? null : Number(this.state.id)
        var location = this.state.location_id === '' ? null : Number(this.state.location_id)

        const body = {
            title: this.state.title,
            isbn: this.state.isbn,
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
            console.log(result)

            this.props.fetchDisplays()

            this.setState({
                title: '',
                isbn: '',
                person_id: '',
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
                    <div className="head-text">Neues Buch</div>
                    <input className="input-field" value={this.state.title} name="title" onChange={(event) => this.updateWithEvent(event)} placeholder="Titel"></input>
                    <input className="input-field" value={this.state.isbn} name="isbn" onChange={(event) => this.updateWithEvent(event)} placeholder="ISBN"></input>
                    <select className="input-field-dropdown" value={this.state.selectedPerson} onChange={this.handlePersonChange}>
                        <option value="" disabled defaultValue hidden>Person auswählen</option>
                        {this.state.personen.map((personen, key) => {
                            return <option key={key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
                        })}
                    </select>

                    <input className="input-field" value={this.state.location_id} name="location_id" onChange={(event) => this.updateWithEvent(event)} placeholder="Standort"></input>
                    <div className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
                </div>
            </div>
        )
    }
}