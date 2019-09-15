package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;
import yeah.hack.filizanka.model.Point;

@Repository
@Transactional(propagation = Propagation.MANDATORY)
public interface PointRepository extends JpaRepository<Point, Long> {

    Point findByStationName(String stationName);

}
