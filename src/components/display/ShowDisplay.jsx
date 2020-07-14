import React from 'react';
import '../notebook/AllNotebook.css'
import AddDisplay from './AddDisplay';
import DisplayModal from '../display/DisplayModal';
const { fetchDisplays } = require('../../util/HttpHelper')


export default class ShowDisplay extends React.Component {

    state = {
        allDisplays: [],
        loading: true,
        open: false,
        showUpdate: false,
        toUpdate: null
    };

    async componentDidMount() {
        this.fetchDisplays()
    }

    fetchDisplays = async () => {
        const displays = await fetchDisplays()
        this.setState({ allDisplays: displays });
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
                fetch("http://192.168.0.94:8015/material/display/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchDisplays)

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
                    <div className="line-text">Bildschirme</div>
                    <div className="line2"></div>
                </div>
                <AddDisplay fetchDisplays={this.fetchDisplays} />
                {this.state.allDisplays.map(allDisplays => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Bildschirm</div>
                            <div>Marke: {allDisplays.make}</div><br></br>
                            <div>Modell: {allDisplays.model}</div><br></br>
                            <div>SN: {allDisplays.serial_number}</div><br></br>
                            <div>Standort: {allDisplays.classroom && allDisplays.classroom.addressloc != null ? allDisplays.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allDisplays.classroom != null ? allDisplays.classroom.room : ""}</div><br></br>
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allDisplays.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allDisplays)}>Ändern</div>
                            </div>

                            {this.state.toUpdate != null && <DisplayModal fetchDisplays={this.fetchDisplays} toUpdate={this.state.toUpdate} open={open} onClose={this.onCloseModal} center showCloseIcon={false}>
                            </DisplayModal>}

                        </div>
                    </div>
                )
                )}
            </div>
        )
    }
}