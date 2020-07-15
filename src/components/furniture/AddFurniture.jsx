import React from 'react';
const { fetchRooms } = require('../../util/HttpHelper');

export default class AddFurniture extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            type: '',
            quantity: '',
            location_id: '',
            rooms: [],
            paramsRoom: { id: null },
            selectedRoom: '',
        }
    }

    async componentDidMount() {
        this.fetchRooms()
    }

    fetchRooms = async () => {
        const rooms = await fetchRooms()
        this.setState({ rooms: rooms });
        console.log(rooms)
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

        var location = this.state.location_id === '' ? null : Number(this.state.location_id)

        const body = {
            type: this.state.type,
            quantity: this.state.quantity,
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

            let result = await fetch('http://192.168.0.94:8015/material/furniture', req)
            const r = await result.text()
            if (r !== "")
                alert(r)

            this.props.fetchFurniture()

            this.setState({
                type: '',
                quantity: '',
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
                    <div className="head-text">Neues Mobiliar</div>
                    <input className="input-field" value={this.state.type} name="type" onChange={(event) => this.updateWithEvent(event)} placeholder="Art"></input>
                    <input type="number" min="1" className="input-field" value={this.state.quantity} name="quantity" onChange={(event) => this.updateWithEvent(event)} placeholder="Anzahl"></input>
                   
                    <select className="input-field-dropdown" value={this.state.selectedRoom} onChange={this.handleRoomChange}>
                        <option value="" defaultValue >Raum auswählen</option>
                        {this.state.rooms.map((rooms, key) => {
                            var x = rooms.adresslocations[0] != undefined ? " / " + rooms.adresslocations[0].address.place : ""
                            return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + x}</option>
                        })}
                    </select>

                    <div className="add-button" onClick={() => this.postData()}>Hinzufügen</div>
                </div>
            </div>
        )
    }
}