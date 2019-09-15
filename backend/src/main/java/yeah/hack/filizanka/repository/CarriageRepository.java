package yeah.hack.filizanka.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import yeah.hack.filizanka.model.Carriage;

@Repository
public interface CarriageRepository extends JpaRepository<Carriage, Long> {
}
