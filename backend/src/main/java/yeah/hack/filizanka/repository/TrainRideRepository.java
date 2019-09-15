package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.TrainRide;

@Repository
@Transactional(propagation = Propagation.MANDATORY)
public interface TrainRideRepository extends JpaRepository<TrainRide, String> {
}
