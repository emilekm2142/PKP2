package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Train;

@Repository
@Transactional(propagation = Propagation.MANDATORY)
public interface TrainRepository extends JpaRepository<Train, Long> {
}
