import React from 'react';
import '../notebook/AllNotebook.css'
import AddEquipment from './AddEquipment';
import EquipmentModal from './EquipmentModal'
const { fetchEquipment } = require('../../util/HttpHelper')

export default class ShowEquipment extends React.Component {

    state = {
        allEquipment: [],
        loading: true,
        open: false,
        showUpdate: false,
        toUpdate: null,
        params: { id: null },
        selectedPerson: ''
    };

    async componentDidMount() {
        this.fetchEquipment()
    }

    fetchEquipment = async () => {
        const equipment = await fetchEquipment()
        this.setState({ allEquipment: equipment });
        console.log(equipment)
    }

    onOpenModal = (toUpdate) => {
        this.setState({
            open: true,
            toUpdate: toUpdate
        });
    }
    onCloseModal = () => {
        this.setState({
            open: false,
            toUpdate: null
        });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/equipment/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchEquipment)

            } catch (error) {
                console.log(error)
            }
        }
    }

    render() {
        if (this.props.isShowing === false) {
            return null;
        }

        const { open } = this.state;

        return (
            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div>
                    <div className="line-text">Zubehör</div>
                    <div className="line2"></div>
                </div>
                <AddEquipment fetchDisplays={this.fetchEquipment} />
                {this.state.allEquipment.map(allEquipment => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="main-text">
                                <div className="head-text">Zubehör</div>
                                <div>Art: {allEquipment.type}</div><br></br>
                                <div>Marke: {allEquipment.make}</div><br></br>
                                <div>Modell: {allEquipment.model}</div><br></br>
                                <div>Anzahl: {allEquipment.quantity}</div><br></br>
                                <div>Person: {allEquipment.person != null ? allEquipment.person.name1 + " " + allEquipment.person.name2 : ""}</div><br></br>
                                <div>Standort: {allEquipment.classroom && allEquipment.classroom.addressloc != null && allEquipment.classroom.addressloc.address != null ? allEquipment.classroom.addressloc.address.place : ""}</div><br></br>
                                <div>Räumlichkeit: {allEquipment.classroom != null ? allEquipment.classroom.room : ""}</div><br></br>
                            </div>
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allEquipment.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allEquipment)}>Ändern</div>
                            </div>

                            {this.state.toUpdate != null && <EquipmentModal fetchEquipment={this.fetchEquipment} toUpdate={this.state.toUpdate} personToUpdate={this.state.toUpdate.person} open={open} onClose={this.onCloseModal} center showCloseIcon={false}>
                            </EquipmentModal>}

                        </div>
                    </div>
                )
                )}
            </div>
        )
    }
}