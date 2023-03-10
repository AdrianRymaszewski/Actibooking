import { useContext } from "react";
import { Link } from "react-router-dom";
import styles from "./NavigationBar.module.css";
import AccountContext from "../../../Context/account-ctx";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightFromBracket } from "@fortawesome/free-solid-svg-icons";
import {faGear, faUser, faCalendarDays, faWallet, faPersonRunning} from "@fortawesome/free-solid-svg-icons";

const NavigationBar = () => {
  const account_ctx = useContext(AccountContext);


  return (
      <div className={styles.Navigation}>
        <div
          className={styles.ButtonIcon}
          onClick={() => account_ctx.setBody({isProfilePage:true})}>
          <FontAwesomeIcon icon={faUser} /> User Panel
        </div>
        {account_ctx.userData.participants &&(
          <div
          className={styles.ButtonIcon}
          onClick={() => account_ctx.setBody({isCoursesPage:true})}>
          <FontAwesomeIcon icon={faCalendarDays} /> Your activities
        </div>
        )}
        <div className={styles.ButtonIcon}>
          <FontAwesomeIcon icon={faWallet} /> Payments
        </div>
        <div
          className={styles.ButtonIcon}
          onClick={() => account_ctx.setBody({isSettingsPage:true})}>
          <FontAwesomeIcon icon={faGear} /> Settings
        </div>
        {account_ctx.userData.isTrainer && (
          <div
            className={styles.ButtonIcon}
            onClick={() => account_ctx.setBody({isTrainerPage:true})}>
            <FontAwesomeIcon icon={faPersonRunning} /> Trainer Page
          </div>
        )}
        <div className={styles.logOutIconContainer}>
          <Link to={"/"} className={styles.LogOutIcon} onClick={account_ctx.logOut}>
            <FontAwesomeIcon icon={faRightFromBracket} /> Log out
          </Link>
        </div>
      </div>
  );
};

export default NavigationBar;
