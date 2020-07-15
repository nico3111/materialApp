import React from 'react';
import UpdateModal from '../updateModal/UpdateModal';
const { fetchDisplays, fetchRooms } = require('../../util/HttpHelper');

export default class DisplayModal extends React.Component {

    state = {

        rooms: [],
        paramsRoom: { id: null },
        selectedRoom: '',
        toUpdate: this.props.toUpdate,
        location_id: null
    }

    async componentDidMount() {
        this.fetchRooms()
    }

    fetchRooms = async () => {
        const rooms = await fetchRooms()
        this.setState({ rooms: rooms });
    }

    updateWithEvent(event) {
        const key = event.target.name;
        const value = event.target.value;

        this.setState(prev => ({
            toUpdate: {
                ...prev.toUpdate,
                [key]: value
            }
        }))

        console.log(this.state.toUpdate)
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
                location_id: null
            })
        }
    }

    async putData() {

        var location_id = this.state.location_id === null ? null : Number(this.state.location_id)

        const body = {
            id: this.state.toUpdate.id,
            model: this.state.toUpdate.model,
            make: this.state.toUpdate.make,
            serial_number: this.state.toUpdate.serial_number,
            location_id: location_id
        }

        try {
            var req = {
                method: 'put',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(body)
            }

            let result = await fetch('http://192.168.0.94:8015/material/display', req)
            const r = await result.text()
            if (r !== "")
                alert(r)

            this.props.fetchDisplays()

            this.props.onClose();

            console.log(body)
        } catch (error) {
            console.log(error)
        }
    }

    render() {
        return (
            <UpdateModal open={this.props.open} onClose={this.props.onClose}>
                <div className="modal-wrapper">
                    <div className="modal-content">
                        <div className="head-wrapper-modal">
                            <div className="modal-main-text">Display Update</div>
                        </div>
                        <div className="text-above-modal">Marke:</div>
                        <input className="input-modal" value={this.state.toUpdate.make} name="make" onChange={(event) => this.updateWithEvent(event)}></input>
                        <div className="text-above-modal">Modell:</div>
                        <input className="input-modal" value={this.state.toUpdate.model} name="model" onChange={(event) => this.updateWithEvent(event)}></input>
                        <div className="text-above-modal">Seriennummer:</div>
                        <input className="input-modal" value={this.state.toUpdate.serial_number} name="serial_number" onChange={(event) => this.updateWithEvent(event)}></input>
                        <div className="text-above-modal">Standort:</div>
                        <select className="input-field-dropdown-modal" value={this.state.selectedRoom} onChange={this.handleRoomChange}>
                            <option value="" defaultValue >Raum auswählen</option>
                            {this.state.rooms.map((rooms, key) => {
                                var x = rooms.adresslocations[0] != undefined ? " / " + rooms.adresslocations[0].address.place : ""
                                return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + x}</option>
                            })}
                        </select>
                    </div>
                    <div className="button-wrapper-modal">
                        <button className="button-modal" onClick={() => this.putData()}>Ändern</button>
                    </div>
                </div>

            </UpdateModal>
        )
    }
}