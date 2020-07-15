import React from 'react'
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';
import "./modal.css";

export default class UpdateModal extends React.Component {

    render() {
        return (
            <Modal open={this.props.open} onClose={this.props.onClose} classNames={{
                overlay: 'customOverlay',
                modal: 'customModal',
            }}>
                {this.props.children}
            </Modal>
        )
    }
}