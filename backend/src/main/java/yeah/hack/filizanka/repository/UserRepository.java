package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.TrainRide;
import yeah.hack.filizanka.model.User;

import java.util.Set;

@Repository
@Transactional(propagation = Propagation.MANDATORY)
public interface UserRepository extends JpaRepository<User, Long> {

    Set<User> findAllByCurrentTrainRide(TrainRide trainRide);
}
